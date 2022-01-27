$(function () {

    $("table thead th").each(function () {
        var title = $(this).text();
        var count = title.length;
        if (title == "Action") {
            $(this).html('<div style="width:' + (count + 12) + '0px"><lable style="text-align: center;" class="control-lable text-center">' + title + '</lable></div>');
        }
        else {
            $(this).html('<div style="width:' + (count + 5) + '0px"><lable style="text-align: center;" class="control-lable text-center">' + title + '</lable></div>');
        }
    });


    var columns = [];
    for (i = 1; i < $("table > thead > tr:first > th").length; i++) {
        columns.push(i);
    }

    $("table").DataTable({
        "dom": '<"row"<"col-sm-12 col-md-4 text-left"l>><"row"<"col-sm-12 col-md-7"B><"col-sm-12 col-md-5 text-right"f>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        "scrollY": "50vh",
        "scrollX": true,
        "scrollCollapse": true,
        "paging": false,
        "initComplete": function () {
            $('.dataTables_filter input[type="search"]').css({ 'width': '350px', 'display': 'inline-block' });
        }
    });
});