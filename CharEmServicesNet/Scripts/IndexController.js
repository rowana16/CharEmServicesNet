$('#submitLocation').on('click', function (e) {
    var selectedLocations = [];
    $('#selectedLocations option:selected').each(function (i, selected) {
        selectedLocations[i] = $(selected).val();
    });
    
    $.ajax({
        url: "/Home/LocationPartial",
        type: "POST",
        data: { 'selectedLocations': selectedLocations }
    })
    .done(function (partialViewResult) {
        $('#locationPartial').html(partialViewResult);
    });
});