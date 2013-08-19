(function (window, undefined) {
    var userPos;
    var marker;
    var map;

    function initialize() {
        directionsDisplay = new google.maps.DirectionsRenderer();

        var mapOptions = {
            center: new google.maps.LatLng(-23.5251, -46.5810),
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        directionsDisplay.setMap(map);

        // Try HTML5 geolocation
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                userPos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

                map.setCenter(pos);
            }, function () {
                handleNoGeolocation(true);
            });
        } else {
            // Browser doesn't support Geolocation
            handleNoGeolocation(false);
        }

        marker = new google.maps.Marker({
            map: map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: userPos
        });

        //Auto complete
        var ACoptions = { componentRestrictions: { country: 'br' } };
        var autoCompleteStart = new google.maps.places.Autocomplete(document.getElementById('Origem.0.Endereco'), ACoptions);
        var autoCompleteRetirada = new google.maps.places.Autocomplete(document.getElementById('Destino.0.Endereco'), ACoptions);
    }

    function handleNoGeolocation(errorFlag) {
        if (errorFlag) {
            var content = 'Error: The Geolocation service failed.';
        } else {
            var content = 'Error: Your browser doesn\'t support geolocation.';
        }

        var options = {
            map: map,
            position: new google.maps.LatLng(-23.547778, -46.635833),
            content: content
        };

        map.setCenter(options.position);
    }

    function toggleBounce() {
        if (marker.getAnimation() != null) {
            marker.setAnimation(null);
        } else {
            marker.setAnimation(google.maps.Animation.BOUNCE);
        }
    }

    google.maps.event.addDomListener(window, 'load', initialize);

})(window);