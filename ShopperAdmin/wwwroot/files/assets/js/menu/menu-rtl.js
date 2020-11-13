$(document).ready(function () {
    $("#mboard").mboardmenu({
        themelayout: 'vertical',
        verticalMenuplacement: 'right', // value should be left/right
        verticalMenulayout: 'wide', // value should be wide/box
        MenuTrigger: 'click', // click / hover
        SubMenuTrigger: 'click', // click / hover
        activeMenuClass: 'active',
        ThemeBackgroundPattern: 'pattern4',  // pattern1, pattern2, pattern3, pattern4, pattern5, pattern6
        HeaderBackground: 'theme1',  // theme1, theme2, theme3, theme4, theme5  header color
        LHeaderBackground: 'theme1', // theme1, theme2, theme3, theme4, theme5, theme6   brand color
        NavbarBackground: 'theme1', // themelight1, theme1  // light  and dark sidebar
        ActiveItemBackground: 'theme1', // theme1, theme2, theme3, theme4, theme5, theme6, theme7, theme8, theme9, theme10, theme11, theme12  mennu active item color
        SubItemBackground: 'theme2',
        ActiveItemStyle: 'style0',
        ItemBorder: true,
        ItemBorderStyle: 'none',
        SubItemBorder: true,
        DropDownIconStyle: 'style1', // Value should be style1,style2,style3
        menutype: 'st6', // Value should be st1, st2, st3, st4, st5 menu icon style
        freamtype: "theme1",
        layouttype:'light', // Value should be light / dark
        FixedNavbarPosition: true,  // Value should be true / false  header postion
        FixedHeaderPosition: true,  // Value should be true / false  sidebar menu postion
        collapseVerticalLeftHeader: true,
        VerticalSubMenuItemIconStyle: 'style1', // value should be style1, style2, style3, style4, style5, style6
        VerticalNavigationView: 'view1',
        verticalMenueffect: {
            desktop: "shrink",
            tablet: "overlay",
            phone: "overlay",
        },
        defaultVerticalMenu: {
            desktop: "expanded", // value should be offcanvas/collapsed/expanded/compact/compact-acc/fullpage/ex-popover/sub-expanded
            tablet: "offcanvas", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
            phone: "offcanvas", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
        },
        onToggleVerticalMenu: {
            desktop: "offcanvas", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
            tablet: "expanded", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
            phone: "expanded", // value should be offcanvas/collapsed/expanded/compact/fullpage/ex-popover/sub-expanded
        },

    });
    function freamtype() {
        $('.theme-color > a.fream-type').on("click", function () {
            var value = $(this).attr("fream-type");
            $('.mboard').attr('fream-type', value);

            $('.mboard-header').attr("header-theme", "themelight"+value);
            $('.mboard-navbar').attr("navbar-theme", "theme" + value);
            $('.navbar-logo').attr("logo-theme", "theme" + value);

        });
    };
    freamtype();
    /* layout type Change function Start */
    function handlelayouttheme() {
        $('.theme-color > a.Layout-type').on("click", function () {
            var layout = $(this).attr("layout-type");
            $('.mboard').attr("layout-type", layout);
            if(layout=='dark'){
                $('.mboard-header').attr("header-theme", "theme6");
                $('.mboard-navbar').attr("navbar-theme", "theme1");
                $('.navbar-logo').attr("logo-theme", "theme6");
                $('body').addClass('dark');

            }
            if(layout=='light'){
                $('.mboard-header').attr("header-theme", "theme1");
                $('.mboard-navbar').attr("navbar-theme", "themelight1");
                $('.navbar-logo').attr("logo-theme", "theme1");
                $('body').removeClass('dark');
            }
        });
    };
    handlelayouttheme();

    /* Left header logo Change function Start */
    function handleogortheme() {
        $('.theme-color > a.logo-theme').on("click", function () {
            var logotheme = $(this).attr("logo-theme");
            $('.navbar-logo').attr("logo-theme", logotheme);
        });
    };
    handleogortheme();

    /* Left header Theme Change function Start */
    function handleleftheadertheme() {
        $('.theme-color > a.leftheader-theme').on("click", function () {
            var lheadertheme = $(this).attr("lheader-theme");
            $('.mboard-navigatio-lavel').attr("menu-title-theme", lheadertheme);
        });
    };
    handleleftheadertheme();
    /* Left header Theme Change function Close */
    /* header Theme Change function Start */
    function handleheadertheme() {
        $('.theme-color > a.header-theme').on("click", function () {
            var headertheme = $(this).attr("header-theme");
            $('.mboard-header').attr("header-theme", headertheme);
            $('.navbar-logo').attr("logo-theme", headertheme);
        });
    };
    handleheadertheme();
    /* header Theme Change function Close */
    /* Navbar Theme Change function Start */
    function handlenavbartheme() {
        $('.theme-color > a.navbar-theme').on("click", function () {
            var navbartheme = $(this).attr("navbar-theme");
            $('.mboard-navbar').attr("navbar-theme", navbartheme);
        });
    };

    handlenavbartheme();
    /* Navbar Theme Change function Close */
    /* Active Item Theme Change function Start */
    function handleactiveitemtheme() {
        $('.theme-color > a.active-item-theme').on("click", function () {
            var activeitemtheme = $(this).attr("active-item-theme");
            $('.mboard-navbar').attr("active-item-theme", activeitemtheme);
        });
    };

    handleactiveitemtheme();
    /* Active Item Theme Change function Close */
    /* SubItem Theme Change function Start */
    function handlesubitemtheme() {
        $('.theme-color > a.sub-item-theme').on("click", function () {
            var subitemtheme = $(this).attr("sub-item-theme");
            $('.mboard-navbar').attr("sub-item-theme", subitemtheme);
        });
    };

    handlesubitemtheme();
    /* SubItem Theme Change function Close */
    /* Theme background pattren Change function Start */
    function handlethemebgpattern() {
        $('.theme-color > a.themebg-pattern').on("click", function () {
            var themebgpattern = $(this).attr("themebg-pattern");
            $('body').attr("themebg-pattern", themebgpattern);
        });
    };

    handlethemebgpattern();
    /* Theme background pattren Change function Close */
    /* Vertical Navigation View Change function start*/
    function handleVerticalNavigationViewChange() {
        $('#navigation-view').val('view1').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard').attr('vnavigation-view', get_value);
        });
    };

    handleVerticalNavigationViewChange();
    /* Theme Layout Change function Close*/

    /* Theme Layout Change function start*/
    function handlethemeverticallayout() {
        $('#theme-layout').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard').attr('vertical-layout', "box");
                $('#bg-pattern-visiblity').removeClass('d-none');

            } else {
                $('.mboard').attr('vertical-layout', "wide");
                $('#bg-pattern-visiblity').addClass('d-none');
            }
        });
    };
    handlethemeverticallayout();
    /* Theme Layout Change function Close*/
    /* Menu effect change function start*/
    function handleverticalMenueffect() {
        $('#vertical-menu-effect').val('shrink').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard').attr('vertical-effect', get_value);
        });
    };

    handleverticalMenueffect();
    /* Menu effect change function Close*/
    /* Vertical Menu Placement change function start*/
    function handleverticalMenuplacement() {
        $('#vertical-navbar-placement').val('left').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard').attr('vertical-placement', get_value);
            $('.mboard-navbar').attr("mboard-navbar-position", 'absolute');
            $('.mboard-header .mboard-left-header').attr("mboard-lheader-position", 'relative');
        });
    };

    handleverticalMenuplacement();
    /* Vertical Menu Placement change function Close*/
    /* Vertical Active Item Style change function Start*/
    function handleverticalActiveItemStyle() {
        $('#vertical-activeitem-style').val('style1').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard-navbar').attr('active-item-style', get_value);
        });
    };

    handleverticalActiveItemStyle();
    /* Vertical Active Item Style change function Close*/
    /* Vertical Item border change function Start*/
    function handleVerticalIItemBorder() {
        $('#vertical-item-border').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard-navbar .mboard-item').attr('item-border', 'false');
            } else {
                $('.mboard-navbar .mboard-item').attr('item-border', 'true');
            }
        });
    };

    handleVerticalIItemBorder();
    /* Vertical Item border change function Close*/
    /* Vertical SubItem border change function Start*/
    function handleVerticalSubIItemBorder() {
        $('#vertical-subitem-border').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard-navbar .mboard-item').attr('subitem-border', 'false');
            } else {
                $('.mboard-navbar .mboard-item').attr('subitem-border', 'true');
            }
        });
    };

    handleVerticalSubIItemBorder();
    /* Vertical SubItem border change function Close*/
    /* Vertical Item border Style change function Start*/
    function handleverticalboderstyle() {
        $('#vertical-border-style').val('solid').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard-navbar .mboard-item').attr('item-border-style', get_value);
        });
    };

    handleverticalboderstyle();
    /* Vertical Item border Style change function Close*/

    /* Vertical Dropdown Icon change function Start*/
    function handleVerticalDropDownIconStyle() {
        $('#vertical-dropdown-icon').val('style1').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard-navbar .mboard-hasmenu').attr('dropdown-icon', get_value);
        });
    };

    handleVerticalDropDownIconStyle();
    /* Vertical Dropdown Icon change function Close*/
    /* Vertical SubItem Icon change function Start*/

    function handleVerticalSubMenuItemIconStyle() {
        $('#vertical-subitem-icon').val('style5').on('change', function (get_value) {
            get_value = $(this).val();
            $('.mboard-navbar .mboard-hasmenu').attr('subitem-icon', get_value);
        });
    };

    handleVerticalSubMenuItemIconStyle();
    /* Vertical SubItem Icon change function Close*/
    /* Vertical Navbar Position change function Start*/
    function handlesidebarposition() {
        $('#sidebar-position').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard-navbar').attr("mboard-navbar-position", 'fixed');
                $('.mboard-header .mboard-left-header').attr("mboard-lheader-position", 'fixed');
            } else {
                $('.mboard-navbar').attr("mboard-navbar-position", 'absolute');
                $('.mboard-header .mboard-left-header').attr("mboard-lheader-position", 'relative');
            }
        });
    };

    handlesidebarposition();
    /* Vertical Navbar Position change function Close*/
    /* Vertical Header Position change function Start*/
    function handleheaderposition() {
        $('#header-position').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard-header').attr("mboard-header-position", 'fixed');
                $('.mboard-navbar').attr("mboard-header-position", 'fixed');
                $('.mboard-main-container').css('margin-top', $(".mboard-header").outerHeight());
            } else {
                $('.mboard-header').attr("mboard-header-position", 'relative');
                $('.mboard-navbar').attr("mboard-header-position", 'relative');
                $('.mboard-main-container').css('margin-top', '0px');
            }
        });
    };
    handleheaderposition();
    /* Vertical Header Position change function Close*/
    /*  collapseable Left Header Change Function Start here*/
    function handlecollapseLeftHeader() {
        $('#collapse-left-header').change(function () {
            if ($(this).is(":checked")) {
                $('.mboard-header, .mboard ').removeClass('iscollapsed');
                $('.mboard-header, .mboard').addClass('nocollapsed');
            } else {
                $('.mboard-header, .mboard').addClass('iscollapsed');
                $('.mboard-header, .mboard').removeClass('nocollapsed');
            }
        });
    };
    handlecollapseLeftHeader();
    /*  collapseable Left Header Change Function Close here*/
});
function handlemenutype(get_value) {
    $('.mboard').attr('nav-type', get_value);
};

handlemenutype("st2");
