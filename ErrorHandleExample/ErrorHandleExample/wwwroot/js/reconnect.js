(function () {
    const observer = new MutationObserver(mutations => {
        mutations.forEach(mutation => {
            if (mutation.target.classList.contains("components-reconnect-rejected") === true) {
                window.location.reload();
            }
        });
    });

    const reconnectModal = document.getElementById("components-reconnect-modal");
    if (reconnectModal !== null) {
        observer.observe(reconnectModal, { attributes: true, subtree: false });
    }
}());

Blazor.start({
    reconnectionOptions: { maxRetries: 17280, retryIntervalMilliseconds: 5000 }
});
