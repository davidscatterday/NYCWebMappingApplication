function btnDeleteMyReport_Click(ReportID) {
    swal("Are you sure you want to delete this report?", {
        buttons: {
            Yes: true,
            No: "No",
        },
    }).then(function (value) {
        switch (value) {
            case "Yes":
                $.ajax({
                    url: RootUrl + 'Home/DeleteReport',
                    data: {
                        ReportID: ReportID
                    },
                    type: "POST",
                    success: function (data) {
                        location.reload();
                    },
                    error: function (error) {
                        console.log("An error occurred from DeleteReport()." + error);
                    }
                });
                break;
            case "No":
                break;
        }
    });
}