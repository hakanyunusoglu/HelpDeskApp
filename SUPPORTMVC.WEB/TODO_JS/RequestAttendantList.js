$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: '/RequestList/FilterAttendant',
        dataType: 'json',
        data: {},
        success: function (filterAttendant) {
                $.each(filterAttendant,
                    function(i, selectedUser) {
                        $("#m_form_Gorevli").append('<option value="' + selectedUser.Value + '">' + selectedUser.Text + '</option>');
                    });
        },
        error: function (ex) {
            alert('Firma bilgilerine ulaşılamadı!' + ex);
        }
    });
    return false;
});