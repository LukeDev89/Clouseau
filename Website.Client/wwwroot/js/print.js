function printHtml(htmlContent) {
    var printWindow = window.open('', '', 'width=800,height=600');
    printWindow.document.write('<html><head><title>Print</title></head><body>');
    printWindow.document.write(htmlContent);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}

function downloadFileFromStream(base64, fileName) {
    const link = document.createElement('a');
    link.href = `data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,${base64}`;
    link.download = fileName;
    link.click();
}

window.scrollToBottom = function (elementId) {
    var container = document.getElementById(elementId);
    if (container) {
        container.scrollTop = container.scrollHeight;
    }
};