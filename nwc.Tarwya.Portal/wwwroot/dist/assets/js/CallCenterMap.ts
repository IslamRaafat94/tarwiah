/**
 * @license
 * Copyright 2019 Google LLC. All Rights Reserved.
 * SPDX-License-Identifier: Apache-2.0
 */
import { MarkerClusterer } from "https://cdn.skypack.dev/@@googlemaps/markerclusterer@2.0.3";

async function initMap(): Promise<void> {
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: { lat: -33, lng: 151 },
    });
    const API = '/Toilets/GetToiletsLookUp';
    const toilets = await fetch(API)
        .then(response => response.json())
        .then(data => data);

    const infoWindow = new google.maps.InfoWindow({
        content: "",
        disableAutoPan: true,
    });

    // Add some markers to the map.
    const markers = toilets.Data.map((position, i) => {
        const image =
            "https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png";
        const beachMarker = new google.maps.Marker({
            position: { lat: parseFloat(position.Latitude), lng: parseFloat(position.Longitude) },
            map,
            icon: image,
            title: position.Code,
            toiletId: position.Id,
            Code: position.Code,
            KedanaCode: position.KedanaCode
        });
        map.addListener("center_changed", () => {
            // 3 seconds after the center of the map has changed, pan back to the
            // marker.
            window.setTimeout(() => {
                map.panTo(beachMarker.getPosition());
            }, 3000);
        });
        beachMarker.addListener("click", () => {
            map.setZoom(8);
            map.setCenter(beachMarker.getPosition());
        });

        return beachMarker;
    });
    // Add a marker clusterer to manage the markers.
    new MarkerClusterer({ markers, map });
}

declare global {
    interface Window {
        initMap: () => void;
    }
}
window.initMap = initMap;
export { };
