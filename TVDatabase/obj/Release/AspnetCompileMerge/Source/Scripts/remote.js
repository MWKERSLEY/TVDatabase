$(function () {
    remoteTwist();
});

function remoteTwist() {
    $html = $('html');
    $remote = $('#remote');
    XAngle = 60;
    YAngle = 0;
    ZAngle = 0;

    $html.on("mouseleave", function () {
        $remote.css({ "transform": "perspective(525px) rotateX(60deg) rotateY(0deg) rotateZ(0deg)", "transition": "all 1000ms ease-in-out 0s", "-webkit-transition": "all 1000ms ease-in-out 0s" });
    });

    $html.on("mousemove", function (e) {
        var XRel = e.pageX - $remote.offset().left;
        var YRel = e.pageY - $remote.offset().top;
        var width = $remote.width();

        YAngle = -6-(0.5 - (XRel / width)) * 15;
        XAngle = 60-(0.5 - (YRel / width)) * 15;
        ZAngle = -6-(0.5 - (XRel / width)) * 15;
        $remote.css({ "transform": "perspective(525px) rotateX(" + XAngle + "deg) rotateY(" + YAngle + "deg) rotateZ(" + ZAngle + "deg)", "transition": "none", "-webkit-transition": "none" });
    });
}