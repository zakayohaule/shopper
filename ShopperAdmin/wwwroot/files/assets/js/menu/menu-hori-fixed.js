
$( document ).ready(function() {
	$('.mboard-navbar .mboard-hasmenu').attr('subitem-icon', 'style1');
	$( "#mboard" ).mboardmenu({
		themelayout: 'horizontal',
		horizontalMenuplacement: 'top',
		horizontalBrandItem: true,
		horizontalLeftNavItem: true,
		horizontalRightItem: true,
		horizontalSearchItem: true,
		horizontalBrandItemAlign: 'left',
		horizontalLeftNavItemAlign: 'left',
		horizontalRightItemAlign: 'right',
		horizontalsearchItemAlign: 'right',
		horizontalMobileMenu: true,
		MenuTrigger: 'hover',
		SubMenuTrigger: 'hover',
		activeMenuClass: 'active',
		ThemeBackgroundPattern: 'pattern6',
		HeaderBackground: 'theme6',
		LHeaderBackground :'theme1',
		NavbarBackground: 'themelight1',
		ActiveItemBackground: 'theme4',
		SubItemBackground: 'theme2',
		menutype: 'st6', // Value should be st1,st2,st3
        freamtype: "theme1",
		ActiveItemStyle: 'style1',
		ItemBorder: true,
		ItemBorderStyle: 'none',
		SubItemBorder: true,
		DropDownIconStyle: 'style1',
		FixedNavbarPosition: true,
		FixedHeaderPosition: true,
		horizontalNavIsCentered: false,
		horizontalstickynavigation: false,
		horizontalNavigationMenuIcon: true,
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

    /* Left header logo Change function Start */
    function handleogortheme() {
        $('.theme-color > a.logo-theme').on("click", function () {
            var logotheme = $(this).attr("logo-theme");
            $('.navbar-logo').attr("logo-theme", logotheme);
        });
    };
    handleogortheme();

	 /* Vertical Header Position change function Start*/
   	function handleheaderposition() {
			$('#header-position').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard-header').attr("mboard-header-position", 'fixed' );
					$('.mboard-navbar').attr("mboard-header-position", 'fixed' );
					$('.mboard-main-container').css('margin-top', $(".mboard-header").outerHeight());
				}else {
					$('.mboard-header').attr("mboard-header-position", 'relative' );
					$('.mboard-navbar').attr("mboard-header-position", 'relative' );
					$('.mboard-main-container').css('margin-top', '0px');
				}
			});
		};

   handleheaderposition ();
 /* Vertical Header Position change function Close*/

function handleheadertheme() {
		$('.theme-color > a.header-theme').on("click", function() {
			var headertheme = $(this).attr("header-theme");
			$('.mboard-header').attr("header-theme", headertheme);
			$('.navbar-logo').attr("logo-theme", headertheme);
        });
    };
    handleheadertheme();
 /* Navbar Theme Change function Start */
	function handlenavbartheme() {
		$('.theme-color > a.navbar-theme').on("click", function() {
			var navbartheme = $(this).attr("navbar-theme");
			$('.mboard-navbar').attr("navbar-theme", navbartheme);
			$('.navbar-logo').attr("navbar-theme", navbartheme);
        });
    };

	handlenavbartheme();
 /* Navbar Theme Change function Close */

 /* Navbar Theme Change function Start */
	function handleActiveItemTheme() {
		$('.theme-color > a.active-item-theme').on("click", function() {
			var AtciveItemTheme = $(this).attr("active-item-theme");
			$('.mboard-navbar').attr("active-item-theme", AtciveItemTheme);
        });
    };

	handleActiveItemTheme();
 /* Navbar Theme Change function Close */


 /* Theme background pattren Change function Start */
	function handlethemebgpattern() {
		$('.theme-color > a.themebg-pattern').on("click", function() {
			var themebgpattern = $(this).attr("themebg-pattern");
			$('body').attr("themebg-pattern", themebgpattern);
        });
    };

	handlethemebgpattern();
 /* Theme background pattren Change function Close */

 /* Theme Layout Change function start*/
	function handlethemehorizontallayout() {
		$('#theme-layout').val('wide').on('change', function (get_value) {
			get_value = $(this).val();
			$('.mboard').attr('horizontal-layout', get_value);
		});
	};

   handlethemehorizontallayout ();
 /* Theme Layout Change function Close*/

 /*Menu Placement change function start*/
   function handleMenuPlacement() {
		$('#navbar-placement').val('top').on('change', function (get_value) {
			get_value = $(this).val();
			$('.mboard').attr('horizontal-placement', get_value);
		});
	};

   handleMenuPlacement ();
 /*Menu Placement change function Close*/



 /*Item border change function Start*/
	function handleIItemBorder() {
			$('#item-border').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard-navbar .mboard-item').attr('item-border', 'false');
				}else {
					$('.mboard-navbar .mboard-item').attr('item-border', 'true');
				}
			});
		};

   handleIItemBorder ();
 /*Item border change function Close*/


 /*SubItem border change function Start*/
   function handleSubIItemBorder() {
			$('#subitem-border').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard-navbar .mboard-item').attr('subitem-border', 'false');
				}else {
					$('.mboard-navbar .mboard-item').attr('subitem-border', 'true');
				}
			});
		};

   handleSubIItemBorder ();
 /*SubItem border change function Close*/


 /*Item border Style change function Start*/
   function handlBoderStyle() {
		$('#vertical-border-style').val('solid').on('change', function (get_value) {
			get_value = $(this).val();
			$('.mboard-navbar .mboard-item').attr('item-border-style', get_value);
		});
	};

   handlBoderStyle ();
 /*Item border Style change function Close*/


// demo 12 st

/* Vertical Dropdown Icon change function Start*/
	 function handleVerticalDropDownIconStyle() {
	   $('#vertical-dropdown-icon').val('style1').on('change', function (get_value) {
		   get_value = $(this).val();
		   $('.mboard-navbar .mboard-hasmenu').attr('dropdown-icon', get_value);
	   });
   };

  handleVerticalDropDownIconStyle ();
/* Vertical Dropdown Icon change function Close*/
/* Vertical SubItem Icon change function Start*/

   function handleVerticalSubMenuItemIconStyle() {
	   $('#vertical-subitem-icon').val('style5').on('change', function (get_value) {
		   get_value = $(this).val();
		   $('.mboard-navbar .mboard-hasmenu').attr('subitem-icon', get_value);
	   });
   };

  handleVerticalSubMenuItemIconStyle ();
/* Vertical SubItem Icon change function Close*/

// demo 12 ed

 /* Horizontal Navbar Position change function Start*/
	function handleNavigationPosition() {
			$('#sidebar-position').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard-navbar').attr("mboard-navbar-position", 'fixed' );
				}else {
					$('.mboard-navbar').attr("mboard-navbar-position", 'relative' );
				}
			});
		};

   handleNavigationPosition ();

 /* Horizontal Navbar Position change function Close*/
 /* Hide Show Menu Icon */
 	function handleNavigationMenuIcon() {
			$('#menu-icons').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard .mboard-navbar .mboard-item > li > a .mboard-micon:not(".mboard-search-item .mboard-micon")').hide();
				}else {
					$('.mboard .mboard-navbar .mboard-item > li > a .mboard-micon:not(".mboard-search-item .mboard-micon")').show();
				}
			});
		};

	handleNavigationMenuIcon ();
   /* Hide Show Brand logo */
    function handlemboardBrandVisibility() {
			$('#brand-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard .mboard-navbar .mboard-brand').hide();
				}else {
					$('.mboard .mboard-navbar .mboard-brand').show();
				}
			});
		};

	handlemboardBrandVisibility ();
	function handlemboardLeftItemVisibility() {
			$('#leftitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard .mboard-navbar .mboard-item.mboard-left-item').hide();
				}else {
					$('.mboard .mboard-navbar .mboard-item.mboard-left-item').show();
				}
			});
		};
	handlemboardLeftItemVisibility ();
	function handlemboardRightItemVisibility() {
			$('#rightitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard .mboard-navbar .mboard-item.mboard-right-item').hide();
				}else {
					$('.mboard .mboard-navbar .mboard-item.mboard-right-item').show();
				}
			});
		};
	handlemboardRightItemVisibility ();
	function handlemboardSearchItemVisibility() {
			$('#searchitem-visibility').change(function() {
				if( $(this).is(":checked")) {
					$('.mboard .mboard-navbar .mboard-item.mboard-search-item').hide();
				}else {
					$('.mboard .mboard-navbar .mboard-item.mboard-search-item').show();
				}
			});
		};
	handlemboardSearchItemVisibility ();

	function handleBrandItemAlign() {
		$('#branditem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.mboard-navbar .mboard-brand').removeClass('mboard-right-align');
				$('.mboard-navbar .mboard-brand').addClass('mboard-left-align');
			}else{
				$('.mboard-navbar .mboard-brand').addClass('mboard-right-align');
				$('.mboard-navbar .mboard-brand').removeClass('mboard-left-align');
			}
		});
	};

   handleBrandItemAlign ();
   function handleLeftItemAlign() {
		$('#leftitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.mboard-navbar .mboard-left-item').removeClass('mboard-right-align');
				$('.mboard-navbar .mboard-left-item').addClass('mboard-left-align');
			}else{
				$('.mboard-navbar .mboard-left-item').addClass('mboard-right-align');
				$('.mboard-navbar .mboard-left-item').removeClass('mboard-left-align');
			}
		});
	};

   handleLeftItemAlign ();
   function handleRightItemAlign() {
		$('#rightitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.mboard-navbar .mboard-right-item').removeClass('mboard-right-align');
				$('.mboard-navbar .mboard-right-item').addClass('mboard-left-align');
			}else{
				$('.mboard-navbar .mboard-right-item').addClass('mboard-right-align');
				$('.mboard-navbar .mboard-right-item').removeClass('mboard-left-align');
			}
		});
	};

   handleRightItemAlign ();
   function handleSearchItemAlign() {
		$('#searchitem-align').val('left').on('change', function (get_value) {
			get_value = $(this).val();
			if (get_value === "left"){
				$('.mboard-navbar .mboard-search-item').removeClass('mboard-right-align');
				$('.mboard-navbar .mboard-search-item').addClass('mboard-left-align');
			}else{
				$('.mboard-navbar .mboard-search-item').addClass('mboard-right-align');
				$('.mboard-navbar .mboard-search-item').removeClass('mboard-left-align');
			}
		});
	};

   handleSearchItemAlign ();
});
function handlemenutype(get_value) {
    $('.mboard').attr('nav-type', get_value);
};

handlemenutype("st2");
