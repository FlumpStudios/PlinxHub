

        $(window).scroll(function () {
            if ($(document).scrollTop() > 50) {
                $('nav').addClass('shrink');
              
            } else {
                $('nav').removeClass('shrink');
            }
        });

        $(document).ready(function () {
           
            $('.title4').textillate();
            $('.title4').textillate({
                in: {
                    effect: 'rollIn'
                }
            });
            $('.parallax').parallax();
    
        });
  


        $(function () {

            var $window = $(window),
                win_height_padded = $window.height() * 1.1,
                isTouch = Modernizr.touch;

            if (isTouch) { $('.revealOnScroll').addClass('animated'); }

            $window.on('scroll', revealOnScroll);
             
            function revealOnScroll() {
                var scrolled = $window.scrollTop(),
                    win_height_padded = $window.height() * 1.0;
             
                // Showed...
                $(".revealOnScroll:not(.animated)").each(function () {
                    var $this = $(this),
                        offsetTop = $this.offset().top;
                   
                    if (scrolled + win_height_padded > offsetTop) {
                        if ($this.data('timeout')) {
                            window.setTimeout(function () {
                                $this.addClass('animated ' + $this.data('animation'));
                            }, parseInt($this.data('timeout'), 10));
                        } else {
                            $this.addClass('animated ' + $this.data('animation'));
                          
                        
                        }
                    }
                });
                // Hidden...
                $(".revealOnScroll.animated").each(function (index) {
                    var $this = $(this),
                        offsetTop = $this.offset().top;
                    if (scrolled + win_height_padded < offsetTop) {
                        $(this).removeClass('animated fadeInUp flipInX lightSpeedIn bounceIn bounceInRight fadeInRight flip fadeInLeft')
                    }
                });
            }

            revealOnScroll();
        });

        function toTop() {
            $("html, body").animate({ scrollTop: 0 }, 900);
        }

        $(function () {
            $(window).scroll(function () {
                $('.title2').each(function (i) {
                    var bottom_of_object = $(this).position().top + $(this).outerHeight();
                    var bottom_of_window = $(window).scrollTop() + $(window).height();

                    /* Adjust the "200" to either have a delay or that the content starts fading a bit before you reach it  */
                    bottom_of_window = bottom_of_window + 100;

                    if (bottom_of_window > bottom_of_object) {
                        $('.title2').textillate();
                              $('.title2').textillate({
                                in: {
                                    effect: 'bounceInRight'
                                }
                            });                    
                    }
                });

            });
        });

        $(function () {
            $(window).scroll(function () {
                $('.title3').each(function (i) {
                    var bottom_of_object = $(this).position().top + $(this).outerHeight();
                    var bottom_of_window = $(window).scrollTop() + $(window).height();

                    /* Adjust the "200" to either have a delay or that the content starts fading a bit before you reach it  */
                    bottom_of_window = bottom_of_window + 100;

                    if (bottom_of_window > bottom_of_object) {
                        $('.title3').textillate();
                        $('.title3').textillate({
                            in: {
                                effect: 'bounceInLeft'
                            }
                        });
                    }
                });

            });
        });

        $(function () {
            $(window).scroll(function () {
                $('.title5').each(function (i) {
                    var bottom_of_object = $(this).position().top + $(this).outerHeight();
                    var bottom_of_window = $(window).scrollTop() + $(window).height();

                    /* Adjust the "200" to either have a delay or that the content starts fading a bit before you reach it  */
                    bottom_of_window = bottom_of_window + 100;

                    if (bottom_of_window > bottom_of_object) {
                        $('.title5').textillate();
                        $('.title5').textillate({
                            in: {
                                effect: 'flip'
                            }
                        });
                    }
                });

            });
        });

    
            $(document).ready(function () {
                $('.carousel').carousel();
                //$(".button-collapse").sideNav();
                $('.slider').slider();
                $('.materialboxed').materialbox();

                $("#PrimaryBrandColour").spectrum({
                    color: "#00f"
                });
                $("#SecondaryBrandColour").spectrum({
                    color: "#f00"
                });
            });


       