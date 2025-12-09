// Geolocation service for browser-based location tracking

export function isGeolocationSupported() {
    return 'geolocation' in navigator;
}

export function getCurrentPosition() {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject(new Error('Geolocation is not supported by this browser.'));
            return;
        }

        const options = {
            enableHighAccuracy: true,
            timeout: 10000,
            maximumAge: 0
        };

        navigator.geolocation.getCurrentPosition(
            (position) => {
                resolve({
                    coords: {
                        latitude: position.coords.latitude,
                        longitude: position.coords.longitude,
                        accuracy: position.coords.accuracy,
                        altitude: position.coords.altitude,
                        altitudeAccuracy: position.coords.altitudeAccuracy,
                        heading: position.coords.heading,
                        speed: position.coords.speed
                    },
                    timestamp: position.timestamp
                });
            },
            (error) => {
                reject(new Error(`Geolocation error: ${error.message}`));
            },
            options
        );
    });
}

export function watchPosition(dotNetHelper) {
    if (!navigator.geolocation) {
        dotNetHelper.invokeMethodAsync('OnLocationError', 'Geolocation is not supported by this browser.');
        return -1;
    }

    const options = {
        enableHighAccuracy: true,
        timeout: 10000,
        maximumAge: 0
    };

    const watchId = navigator.geolocation.watchPosition(
        (position) => {
            const positionData = {
                coords: {
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                    accuracy: position.coords.accuracy,
                    altitude: position.coords.altitude,
                    altitudeAccuracy: position.coords.altitudeAccuracy,
                    heading: position.coords.heading,
                    speed: position.coords.speed
                },
                timestamp: position.timestamp
            };
            dotNetHelper.invokeMethodAsync('OnLocationUpdate', positionData);
        },
        (error) => {
            dotNetHelper.invokeMethodAsync('OnLocationError', error.message);
        },
        options
    );

    return watchId;
}

export function clearWatch(watchId) {
    if (watchId >= 0 && navigator.geolocation) {
        navigator.geolocation.clearWatch(watchId);
    }
}
