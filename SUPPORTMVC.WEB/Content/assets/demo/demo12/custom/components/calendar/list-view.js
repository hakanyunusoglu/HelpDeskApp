var CalendarListView = {
    init: function () {
        var t = moment().startOf("day"),
            e = t.format("YYYY-MM"),
            i = t.clone().subtract(1, "day").format("YYYY-MM-DD"),
            r = t.format("YYYY-MM-DD"),
            n = t.clone().add(1, "day").format("YYYY-MM-DD");
        $("#m_calendar").fullCalendar({
           
            defaultView: "listWeek",
            editable: !1,
            eventLimit: !1,
            navLinks: !1,
            height: 650,
            events: [{
                title: "HBSoft",
                start: r + "T10:30:00",
                end: r + "T12:30:00",
                className: "m-fc-event--danger",
                description: "Görev Listesi Düzenlenecek."
            }, {
                title: "Pleksan",
                start: r + "T12:00:00",
                className: "m-fc-event--info",
                description: "Lorem ipsum dolor sit amet, ut labore"
            }, {
                title: "BAFA",
                start: r + "T14:30:00",
                className: "m-fc-event--warning",
                description: "Lorem ipsum conse ctetur adipi scing"
            }, {
                title: "Ortaç",
                start: r + "T17:30:00",
                className: "m-fc-event--metal",
                description: "Lorem ipsum dolor sit amet, conse ctetur"
            }, {
                title: "HBS",
                start: n + "T07:00:00",
                className: "m-fc-event--primary",
                description: "Lorem ipsum dolor sit amet, scing"
            }],
            eventRender: function (t, e) {
                e.hasClass("fc-day-grid-event") ? (e.data("content", t.description), e.data("placement", "top"), mApp.initPopover(e)) : e.hasClass("fc-time-grid-event") ? e.find(".fc-title").append('<div class="fc-description">' + t.description + "</div>") : 0 !== e.find(".fc-list-item-title").lenght && e.find(".fc-list-item-title").append('<div class="fc-description">' + t.description + "</div>")
            }
        })
    }
};
jQuery(document).ready(function () {
    CalendarListView.init()
});