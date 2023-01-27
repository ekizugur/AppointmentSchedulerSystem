$(document).ready(function () {
    $('#appointmentsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "No data available in the table",
            "sInfo": "Showing records _START_ to _END_ of _TOTAL_",
            "sInfoEmpty": "No record",
            "sInfoFiltered": "(found in the _MAX_ record)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Show _MENU_ record on page",
            "sLoadingRecords": "Loading...",
            "sProcessing": "Processing...",
            "sSearch": "Search:",
            "sZeroRecords": "No matching records found",
            "oPaginate": {
                "sFirst": "First",
                "sLast": "End",
                "sNext": "Next",
                "sPrevious": "Previus"
            },
            "oAria": {
                "sSortAscending": ": enable ascending column sort",
                "sSortDescending": ": enable descending column sort"
            },
            "select": {
                "rows": {
                    "_": "%d record selected",
                    "0": "",
                    "1": "1 record selected"
                }
            }
        }
    });
});