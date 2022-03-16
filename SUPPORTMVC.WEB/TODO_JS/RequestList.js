$(document).ready(function () {
    $('#html_table').DataTable({
            "ajax": {
                "url": "/RequestList/RequestJsonList",
                "type": "GET",
                "dataType": "json",
                "dataSrc": ""
            },
            "columns": [
                { "data": "RequestDate" },
                { "data": "RequestNumber" },
                { "data": "CarrierTrackingNumber" },
                { "data": "reqType" },
                { "data": "reqCompany" },
                { "data": "reqUser" },
                { "data": "reqPriority" },
                { "data": "reqStatus" },
                { "data": "RequestAttendant" },
                { "data": " " }],
            "columnDefs":
                [
                    {
                        "targets": -1,
                        "data": null,
                        "defaultContent":
                            '<a href="/RequestList/RequestDetails/@aktiftalep.RequestID" class="m-portlet__nav-link btn m-btn m-btn--hover-brand m-btn--icon m-btn--icon-only m-btn--pill" title="Detaylar">< i class="fa flaticon-list"></i></a ><a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="modal" data-target="#m_modal_4" data-request-id="@aktiftalep.RequestID" title="Kapat"><i class="fa fa-window-close"></i></a>'
                    }
                ]
        });
});