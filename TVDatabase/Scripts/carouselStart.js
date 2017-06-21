$(document).ready(cStart);

function cStart() {
    var owl = $(".owl-carousel");
    owl.owlCarousel({
        loop: false,
        margin: 15,
        autoplay: 2500,
        responsiveClass:true,
        dots: true,
        center: true,
        //stagePadding: 100,
        responsive:{
            0:{
                items: 1.4,
                //loop: false
            },
            600:{
                items: 2.4,
                //loop: true
            },
            1000:{
                items:3.6,
                //loop: true
            }
        }
    });
    owl.on('mousewheel', '.owl-stage', function (e) {
        if (e.deltaY > 0) {
            owl.trigger('next.owl');
        } else {
            owl.trigger('prev.owl');
        }
        e.preventDefault();
    });
}