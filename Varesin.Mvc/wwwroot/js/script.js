$(function () {
  $('#bootstrap-touch-slider').bsTouchSlider();

  toastr.options = {
    "closeButton": true,
    "debug": false,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": false,
    "onclick": null,
    "timeOut": "5000",
  }

  $("[data-role='date']").pDatepicker({
    format: 'YYYY/MM/DD',
  });

  $('form').on('submit', function () {
    var l = Ladda.create(document.querySelector('.ladda-button'));
    l.start();
  })

  $('[data-role="confirm"]').on('click', function () {
    return confirm('آیا مطمئن هستید ؟؟');
  })

  var elements = document.getElementsByTagName("INPUT");
  for (var i = 0; i < elements.length; i++) {
    elements[i].oninvalid = function (e) {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid) {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements[i].oninput = function (e) {
      e.target.setCustomValidity("");
    };
  }

  var elements = document.getElementsByTagName("SELECT");
  for (var i = 0; i < elements.length; i++) {
    elements[i].oninvalid = function (e) {
      e.target.setCustomValidity("");
      if (!e.target.validity.valid) {
        e.target.setCustomValidity("این فیلد نمی تواند فاقد مقدار باشد ");
      }
    };
    elements[i].oninput = function (e) {
      e.target.setCustomValidity("");
    };
  }

  $('.project__slick').slick({
    infinite: false,
    centerMode: false,
    centerPadding: '60px',
    slidesToShow: 4,
    arrows: false,
    focusOnSelect: false,
    accessibility: false,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '40px',
          slidesToShow: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '40px',
          slidesToShow: 1
        }
      }
    ]
  });

  $('[data-role="slick-left"]').on('click', function () {
    $('.project__slick').slick('slickPrev');
  })
  $('[data-role="slick-right"]').on('click', function () {
    $('.project__slick').slick('slickNext');
  })

  var pathname = window.location.pathname.toLowerCase()

  $('.nav.navbar-nav > li > a').map(function (index, item) {
    var href = $(item).attr('href').toLowerCase();
    if (href === pathname) {
      $(item).closest('li').addClass('active')
    }
  })
})