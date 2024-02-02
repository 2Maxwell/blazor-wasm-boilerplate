

window.downloadReportFile()
{
    var wirterformat = getWriterFormat();
    location.href = 'api/BoldReportWriter/Export?writerFormat=' + wirterformat;
};

function getWriterFormat() {
    var formatType = "";
    if ($('#rbtnPDf').is(':checked')) {
        formatType = $('#rbtnPDf').val();
    } else if ($('#rbtnWord').is(':checked')) {
        formatType = $('#rbtnWord').val();
    } else if ($('#rbtnxls').is(':checked')) {
        formatType = $('#rbtnxls').val();
    } else if ($('#rbtnCSV').is(':checked')) {
        formatType = $('#rbtnCSV').val();
    }
    return formatType;
}