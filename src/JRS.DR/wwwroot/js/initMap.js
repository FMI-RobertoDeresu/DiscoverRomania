"use strict";

(function() {

    var map;
    var markers = [];
    var lat = $("#Location_X").val();
    var lng = $("#Location_Y").val();

    function initMap() {
        map = new window.google.maps.Map(document.getElementById("map"), {
            center: { lat: 45.464862, lng: 25.3388779 },
            zoom: 7
        });

        map.addListener("click", function(e) {
            placeMarker(e.latLng, map);
        });

        if (lat && lng) {
            const myLatLng = { lat: parseFloat(lat), lng: parseFloat(lng) };
            lat = null;
            lng = null;
            const marker = new window.google.maps.Marker({
                position: myLatLng,
                map: map
            });
            markers.push(marker);
        }
    }

    function placeMarker(latLng, map) {
        clearMarkers();

        const marker = new window.google.maps.Marker({
            position: latLng,
            map: map
        });

        markers.push(marker);
        $("#Location_X").val(latLng.lat());
        $("#Location_Y").val(latLng.lng());
    }

    function setMapOnAll(map) {
        for (let i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    }

    function clearMarkers() {
        setMapOnAll(null);
    }

    window.initMap = initMap;

})();