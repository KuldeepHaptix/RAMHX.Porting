
$(document).ready(function () {

    $('#tblHTMlModules').DataTable();

    if ($('[id*=ddlGroups]').val() == '0')
        $('#spanAddNew').hide();

    $('#ddlGroups').change(function () {
        window.location.href = '/Admin/Configurations?id=' + $(this).val();


    });
});