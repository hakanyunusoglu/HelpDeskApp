$(document).ready(function () {
    //Selectlist seçili değer eventi 
    $("#m_select2_1").change(function () {
        $("#m_form_TalepSahibi").empty();
        $.ajax({
            type: 'POST',
            url: '/RequestList/FilterOwner', // json method çağırılıyor 

            dataType: 'json',

            data: { id: $("#m_select2_1 :selected").val() },
            //Seçili firmanın value'sini alıyoruz ve controller e işlem için gönderiyoruz.

            success: function (filterList) {
                //İşlem başarılı ise olmasını istediğimizi buraya yazıyoruz  
                // Aldığımız değer DB de eşleşiyorsa aşağıya firmaya bağlı kullanıcıları çekiyorum.

                $("#m_form_TalepSahibi").append('<option value="0" selected disabled>Seçiniz</option>');

                $.each(filterList, function (i, selectedUser) {
                    $("#m_form_TalepSahibi").append('<option value="' + selectedUser.Value + '">' +
                        selectedUser.Text + '</option>');
                    // Burada her bir değer için option ekliyorum. 

                });
            },
            error: function (ex) {
                alert('Bilgilere ulaşılamadı!' + ex);
            }
        });
        return false;
    })
}); 