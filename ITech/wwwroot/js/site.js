// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




/* Start of slider*/
$(document).ready(function () {
    $('.autoWidth').lightSlider({
        autoWidth: true,
        loop: true,
        onSliderLoad: function () {
            $('.autoWidth').removeClass('cS-hidden');
        }
    });
});

/* End of slider */

