$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var typeToHide = e.relatedTarget.href.split("#")[1].split("-")[0];
    var typeToShow = e.target.href.split("#")[1].split("-")[0];
    console.log(e.target)
    console.log("id of previous active tab: " + typeToHide);
    console.log("id of newly activated tab: " + typeToShow);
    var divToHide = $("#"+typeToHide+"-chart");
    var divToShow = $("#"+typeToShow+"-chart");
    divToHide.hide();
    divToShow.show();
});
