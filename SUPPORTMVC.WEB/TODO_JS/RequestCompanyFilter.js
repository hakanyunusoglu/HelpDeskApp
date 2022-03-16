$(document).ready(function () {
    $("#m_select2_1").empty();
    $.ajax({
        type: 'POST',
        url: '/RequestList/FilterCompany',
        dataType: 'json',
        data: { },
        success: function (filterCompany) {
            $.each(filterCompany,
                function(i, selectedCompany) {
                    $("#m_select2_1").append('<option value="' + selectedCompany.Value + '">' + selectedCompany.Text + '</option>');
                });
        },
        error: function(ex) {
            alert('Firma bilgilerine ulaşılamadı!' + ex);
        }
    });
    return false;
});
$(document).ready(function () {
    $("#m_select2_2").empty();
    $.ajax({
        type: 'POST',
        url: '/RequestList/FilterCompany',
        dataType: 'json',
        data: {},
        success: function (filterCompany) {
            $.each(filterCompany,
                function (i, selectedCompany) {
                    $("#m_select2_2").append('<option value="' + selectedCompany.Value + '">' + selectedCompany.Text + '</option>');
                });
        },
        error: function (ex) {
            alert('Firma bilgilerine ulaşılamadı!' + ex);
        }
    });
    return false;
});