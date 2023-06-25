

window.saveAsFile = (filename, data) => {
    console.log("Loging hello");
    const blob = new Blob([data], { type: "application/octet-stream" });
    if (navigator.msSaveBlob) {
        navigator.msSaveBlob(blob, filename);
    } else {
        const link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.download = filename;
        link.click();
        window.URL.revokeObjectURL(link.href);
    }
};