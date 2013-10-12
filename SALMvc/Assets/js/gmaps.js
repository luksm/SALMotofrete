(function (window, undefined) {
    var userPos;
    var markers = new Array();
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

        //Auto complete
        var ACoptions = { componentRestrictions: { country: 'br' } };
        inputStart = document.getElementById('EnderecoRetirada_Logradouro');
        var autoCompleteStart = new google.maps.places.Autocomplete(inputStart, ACoptions);
        inputEnd = document.getElementById('EnderecoEntrega_Logradouro');
        var autoCompleteEnd = new google.maps.places.Autocomplete(inputEnd, ACoptions);

        // Bound Autocomplete to map 
        autoCompleteStart.bindTo('bounds', map);
        autoCompleteEnd.bindTo('bounds', map);

        // Create markers and info window
        var infowindowStart = new google.maps.InfoWindow();
        var markerStart = new google.maps.Marker({
            animation: google.maps.Animation.DROP,
            map: map
        });
        markerStart.setPosition(userPos);
        markers.push(markerStart);
        var infowindowEnd = new google.maps.InfoWindow();
        var markerEnd = new google.maps.Marker({
            animation: google.maps.Animation.DROP,
            map: map
        });
        markerEnd.setPosition(userPos);
        markers.push(markerEnd);

        // Adiciona o marker de Retirada
        google.maps.event.addListener(autoCompleteStart, 'place_changed', function () {

            infowindowStart.close();
            markerStart.setVisible(false);
            inputStart.className = 'span12';

            var place = autoCompleteStart.getPlace();
            if (!place.geometry) {
                // Inform the user that the place was not found and return.
                inputStart.className = 'span12 notfound';
                return;
            }

            // If the place has a geometry, then present it on a map.
            markerStart.setIcon(/** @type {google.maps.Icon} */({
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(35, 35)
            }));
            markerStart.setPosition(place.geometry.location);
            markerStart.setVisible(true);

            infowindowStart.setContent('<div><strong>Retirada</strong><br>' + place.name);
            infowindowStart.open(map, markerStart);
            setMapBounds();
        });

        // Adiciona o marker de Entrega
        google.maps.event.addListener(autoCompleteEnd, 'place_changed', function () {
            infowindowEnd.close();
            markerEnd.setVisible(false);
            inputEnd.className = 'span12';

            var place = autoCompleteEnd.getPlace();
            if (!place.geometry) {
                // Inform the user that the place was not found and return.
                inputEnd.className = 'span12 notfound';
                return;
            }

            markerEnd.setIcon(/** @type {google.maps.Icon} */({
                url: place.icon,
                animation: google.maps.Animation.DROP,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(35, 35)
            }));
            markerEnd.setPosition(place.geometry.location);
            markerEnd.setVisible(true);

            infowindowEnd.setContent('<div><strong>Entrega</strong><br>' + place.name);
            infowindowEnd.open(map, markerEnd);

            setMapBounds()
        });

    }

    function setMapBounds() {
        if (markers.length == 0)
            return;

        var bounds = new google.maps.LatLngBounds();
        for (index in markers) {
            var data = markers[index];
            if(data.position)
                bounds.extend(new google.maps.LatLng(data.position.lat(), data.position.lng()));
        }
        map.fitBounds(bounds);
        return;
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