window.currentComponent = null;

window.initGoogleAuth = async (clientId, dotNetRef) => {
    // Wait for Google API to load
    while (typeof google === 'undefined' || !google.accounts) {
        await new Promise(resolve => setTimeout(resolve, 100));
    }

    window.currentComponent = dotNetRef;
    google.accounts.id.initialize({
        client_id: clientId,
        callback: handleGoogleResponse
    });
};

function handleGoogleResponse(response) {
    if (window.currentComponent) {
        window.currentComponent.invokeMethodAsync('HandleGoogleLoginCallback', response.credential);
    }
}

window.triggerGoogleLogin = () => {
    if (typeof google !== 'undefined' && google.accounts) {
        google.accounts.id.prompt();
    } else {
        console.error('Google API not loaded yet');
    }
};