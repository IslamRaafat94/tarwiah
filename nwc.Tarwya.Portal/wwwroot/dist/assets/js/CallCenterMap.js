async function initMap() {
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: { lat: -33, lng: 151 },
    });

    const API = '/Toilets/GetToiletsLookUp';
    const toilets = await fetch(API)
        .then(response => response.json())
        .then(data => data);

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
    const markerCluster = new markerClusterer.MarkerClusterer({ map, markers });
}

window.initMap = initMap;

