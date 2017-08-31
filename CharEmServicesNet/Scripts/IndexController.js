$('.serviceTypeBtn').on('click', function (e) {
    
    var selectedServiceType = $(this).attr('id');
    $('#serviceTypeRef').text(selectedServiceType);
    
    $.ajax({
        url: "/Home/LocationPartial/" + selectedServiceType,
        type: "POST"       
    })
    .done(function (partialViewResult) {
        $('#locationPartial').html(partialViewResult);
        $('#serviceType').hide();
    });
});



$(document).ready(function () {
    var $loading = $('#loading').hide();
    $(document)
        .ajaxStart(function () {
            $loading.show();
        })
        .ajaxStop(function () {
            $loading.hide();
        });
});

$(document).on('click','.locationBtn', function (e) {        
    var selectedLocation = $(this).attr('id');
    var selectedServiceType = $('#serviceTypeRef').text();

    $.ajax({
        url: "/Home/ServicePartial/",
        type: "POST",
        data: {
            locationid: selectedLocation,
            servicetypeid: selectedServiceType
        }
    })
    .done(function(partialViewResult){
        $('#serviceDisplay').html(partialViewResult);
        $('#locationPartial').hide();
    })
    
});

