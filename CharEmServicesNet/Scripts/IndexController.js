$('#submitLocation').on('click', function (e) {
    var selectedLocation = $('select[name="selectedLocation"]');
    selectedLocation = selectedLocation[0].value;
    $.ajax({
        url: "/Home/LocationPartial",
        type: "POST",
        data: { 'selectedLocation': selectedLocation }
    })
    .done(function (partialViewResult) {
        $('#locationPartial').html(partialViewResult);
    });
});