var FormRepeater = {
    init: function () {
        $("#m_repeater_1").repeater({
            initEmpty: !1,
            defaultValues: {
                "text-input": "foo"
            },
            show: function () {
                $(this).slideDown()
            },
            hide: function (e) {
                $(this).slideUp(e)
            }
        }), $("#m_repeater_2").repeater({
            initEmpty: !1,
            defaultValues: {
                "text-input": "foo"
            },
            show: function () {
                $(this).slideDown()
            },
            hide: function (e) {
                confirm("Are you sure you want to delete this element?") && $(this).slideUp(e)
            }
        }), $("#m_repeater_3").repeater({
            initEmpty: !1,
            show: function() {
                $(this).slideDown()
            },
            hide: function(e) {
                //swal({
                //    title: "Emin misiniz?",
                //    text: "Bu dosyayı silmek istiyor musunuz?",
                //    type: "warning",
                //    showCancelButton: !0,
                //    showConfirmButton: !0,
                //    confirmButtonText: "Evet",
                //    cancelButtonText: "Hayır"
                //});
                //$('.swal2-confirm').click(function() {
                $(this).slideUp(e)
            }
        }), $("#m_repeater_4").repeater({
            initEmpty: !1,
            defaultValues: {
                "text-input": "foo"
            },
            show: function () {
                $(this).slideDown()
            },
            hide: function (e) {
                $(this).slideUp(e)
            }
        }), $("#m_repeater_5").repeater({
            initEmpty: !1,
            defaultValues: {
                "text-input": "foo"
            },
            show: function () {
                $(this).slideDown()
            },
            hide: function (e) {
                $(this).slideUp(e)
            }
        }), $("#m_repeater_6").repeater({
            initEmpty: !1,
            defaultValues: {
                "text-input": "foo"
            },
            show: function () {
                $(this).slideDown()
            },
            hide: function (e) {
                $(this).slideUp(e)
            }
        })
    }
};
jQuery(document).ready(function () {
    FormRepeater.init()
});