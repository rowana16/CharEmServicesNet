$('#submitLocation').on('click', function (e) {
    var selectedLocations = $('select[name="selectedLocation"]');
    selectedLocation = selectedLocation[0].value;
    $.ajax({
        url: "/Home/LocationPartial",
        type: "POST",
        data: { 'selectedLocations': selectedLocations }
    })
    .done(function (partialViewResult) {
        $('#locationPartial').html(partialViewResult);
    });
});