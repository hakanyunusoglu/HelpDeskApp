jQuery(document).ready(function () {
var DatatableHtmlTableDemo = {
    init: function() {
        var e;
        e = $(".m-datatable").mDatatable({

            "search": {
                input: $("#generalSearch")
            }
        });
    }
};

    DatatableHtmlTableDemo.init()
});