window.currentComponent = null;

window.initGoogleAuth = async (clientId, dotNetRef) => {
    while (typeof google === 'undefined' || !google.accounts) {
        await new Promise(resolve => setTimeout(resolve, 100));
    }

    window.currentComponent = dotNetRef;

    google.accounts.id.initialize({
        client_id: clientId,
        callback: handleCredentialResponse
    });

    const hiddenDiv = document.getElementById('hiddenGoogleButton');
    if (hiddenDiv) {
        google.accounts.id.renderButton(hiddenDiv, {
            theme: 'outline',
            size: 'large'
        });
    }
};

function handleCredentialResponse(response) {
    if (window.currentComponent && response.credential) {
        window.currentComponent.invokeMethodAsync('HandleGoogleCallback', response.credential);
    }
}

window.triggerGoogleSignIn = () => {
    const hiddenDiv = document.getElementById('hiddenGoogleButton');
    if (hiddenDiv) {
        const googleButton = hiddenDiv.querySelector('div[role="button"]');
        if (googleButton) {
            googleButton.click();
        }
    }
};