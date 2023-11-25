



//Navbar Menu

const menuIcon = document.getElementById('menu-icon');
const menuClose = document.getElementById('menu-close');
const navLinks = document.getElementById('nav-linkss');
const paginationSlider = document.querySelector('.swiper-pagination');
const revSliderZIndex = document.querySelector('.rev');

function toggleMenu() {
  if (navLinks.style.display === 'block') {
    navLinks.style.display = 'none';
    menuIcon.style.display = 'block';
    menuClose.style.display = 'none';
    paginationSlider.style.zIndex = "0";
    revSliderZIndex.style.zIndex = "0";
  } else {
    navLinks.style.display = 'block';
    menuIcon.style.display = 'none';
    menuClose.style.display = 'block';
    paginationSlider.style.zIndex = "1";
    revSliderZIndex.style.zIndex = "1";

  }
}

menuIcon.addEventListener('click', toggleMenu);
menuClose.addEventListener('click', () => {
  navLinks.style.display = 'none';
  menuIcon.style.display = 'block';
  menuClose.style.display = 'none';
});

function checkScreenWidth() {
  if (window.innerWidth <= 1024) {
    menuIcon.style.display = 'block';
    menuClose.style.display = 'none';
    navLinks.style.display = 'none';
    menuIcon.addEventListener('click', toggleMenu);
  } else {
    menuIcon.style.display = 'none';
    menuClose.style.display = 'none';
    navLinks.style.display = 'block';
    menuIcon.removeEventListener('click', toggleMenu);
  }
}

window.addEventListener('load', checkScreenWidth);
window.addEventListener('resize', checkScreenWidth);




//bg header slider webkit svg
$(document).ready(function () {
  var customSVG = '<svg width="120" height="2" viewBox="0 0 120 2" fill="none" xmlns="http://www.w3.org/2000/svg">' +
  '<path d="M120 0H0V2H120V0Z" fill="#EF2955"/>' +
  '<path d="M60 0H0V2H60V0Z" fill="white"/>' +
'</svg>';


  // Swiper slider'ı başlatın
  var mySwiper = new Swiper(".swiper-container", {
    autoplay: {
      delay: 1000,
      disableOnInteraction: false
    },
    speed: 500,
    loop: true,
    pagination: {
      el: ".swiper-pagination",
      type: "custom",
      renderCustom: function (swiper, current, total) {
        return '<div class="swiper-pagination-custom"> ' + '<span class="swiper-pagination-current">0' + current + '</span>' +     customSVG +   '<span class="swiper-pagination-total">0' + total + '</span>' + '</div>';
      }
    },
  });
});
 
//Footer Accordion start
const titles = document.querySelectorAll('.footer-list-title');

titles.forEach((title) => {
  title.addEventListener('click', () => {
    title.parentElement.classList.toggle('active');
  });
});
//Footer Accordion end

//Product Detail

document.addEventListener("DOMContentLoaded", function () {
  const thumbnailList = document.getElementById("thumbnailList");
  const bannerImage = document.getElementById("bannerImage");
  const thumbnails = thumbnailList.querySelectorAll("li");
  const prevButton = document.getElementById("prevButton");
  const nextButton = document.getElementById("nextButton");
  let currentIndex = 0;

  function updateActiveThumbnail(index) {
      thumbnails.forEach((thumb, i) => {
          thumb.classList.remove("thumbnail-active");
          if (i === index) {
              thumb.classList.add("thumbnail-active");
          }
      });
  }

  function updateBannerImage(index) {
      bannerImage.src = thumbnails[index].querySelector("img").src;
  }

  prevButton.addEventListener("click", function () {
      currentIndex = (currentIndex - 1 + thumbnails.length) % thumbnails.length;
      updateActiveThumbnail(currentIndex);
      updateBannerImage(currentIndex);
  });

  nextButton.addEventListener("click", function () {
      currentIndex = (currentIndex + 1) % thumbnails.length;
      updateActiveThumbnail(currentIndex);
      updateBannerImage(currentIndex);
  });

  thumbnails.forEach((thumbnail, index) => {
      thumbnail.addEventListener("click", function () {
          currentIndex = index;
          updateActiveThumbnail(currentIndex);
          updateBannerImage(currentIndex);
      });
  });
});


// Custom Dropdown Mehsullar burda basliyir

var selectedItem = document.getElementById("selected-item");

function toggleDropdown() {
  var dropdownContent = document.getElementById("custom-dropdown-content");
  if (dropdownContent.style.display === "block" || dropdownContent.style.display === "") {
    dropdownContent.style.display = "none";
  } else {
    dropdownContent.style.display = "block";
  }
}

function selectItem(selectedElement) {
  var itemText = selectedElement.textContent;
  selectedItem.textContent = itemText;
  var dropdownContent = document.getElementById("custom-dropdown-content");

  selectedElement.style.display = "none";

  var dropdownItems = dropdownContent.querySelectorAll(".custom-dropdown-item");
  dropdownItems.forEach(function(item) {
    if (item !== selectedElement) {
      item.style.display = "block";
    }
  });

  dropdownContent.style.display = "none";
}



// Custom Dropdown Mehsullar burda bitir


//Kateqoriya Slider

$(".slick-slider").slick({
  slidesToShow: 5.1,
  infinite: true,
  slidesToScroll: 1,
  autoplay: true, // Enable autoplay
  autoplaySpeed: 2000, // Set the autoplay speed in milliseconds (2 seconds in this example)
  responsive: [
    {
      breakpoint: 481, 
      settings: {
        slidesToShow: 1,
      }
    },
    {
      breakpoint: 768, 
      settings: {
        slidesToShow: 2,
      }
    },
    {
      breakpoint: 1200, 
      settings: {
        slidesToShow: 3,
      }
    }
  ]
});

//Media Slider
var glider = new Glider(document.querySelector('.glider'), {
  slidesToScroll: 1,
  slidesToShow: 3.7,
  draggable: true,
  arrows: {
    prev: '.glider-prev-1',
    next: '.glider-next-1'
  },
  responsive: [
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 3.7,
        slidesToScroll: 2,
    
      }
    },{
      breakpoint: 900,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 1,
    
      }
    },
    {
      breakpoint: 640,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 1,
    
      }
    },
    {
      breakpoint: 304,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
    
      }
    },
    {
      breakpoint: 0,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
    
      }
    }
    
  ]
});


window.addEventListener('resize', function () {
  if (window.innerWidth <= 481) {
    glider.settings.slidesToShow = 1;
  } else {
    glider.settings.slidesToShow = 3.7;
  }
  glider.refresh(true); 
});

if (window.innerWidth <= 481) {
  glider.settings.slidesToShow = 1;
  glider.refresh(true);
}

$(document).ready(function () {
  $(".media-prev-next-btn button").click(function () {
    $(".media-prev-next-btn button").removeClass("active");
    $(this).addClass("active");
  });
});


$(document).ready(function(){
    $('.customer-logos').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 1500,
        arrows: false,
        dots: false,
        pauseOnHover: false,
        responsive: [{
            breakpoint: 768,
            settings: {
                slidesToShow: 4
            }
        }, {
            breakpoint: 520,
            settings: {
                slidesToShow: 3
            }
        }]
    });
});



 // Media Slider Start
 
  $('#ex1-tab-2').on('shown.bs.tab', function (e) {
    console.log("test");
    var tabId = e.target.getAttribute('aria-controls'); // Get the ID of the active tab
    if (tabId === 'ex1-tabs-2') {
      new Glider(document.getElementById('glide-2'), {
        slidesToScroll: 1,
        slidesToShow: 3.7,
        draggable: true,
        arrows: {
          prev: '.glider-prev-2',
          next: '.glider-next-2'
        },
        responsive: [
          {
            breakpoint: 1200,
            settings: {
              slidesToShow: 3.7,
              slidesToScroll: 2,

            }
          }, {
            breakpoint: 900,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 640,
            settings: {
              slidesToShow: 2,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 304,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 0,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,

            }
          }

        ]
      });

      // Manipulate the CSS styles
      $('#glider1').css('display', 'none');
      $('#glider2').css('display', 'flex');
      $('#glider3').css('display', 'none');

    }
  });

  $('#ex1-tab-3').on('shown.bs.tab', function (e) {
    var tabId = e.target.getAttribute('aria-controls'); // Get the ID of the active tab
    if (tabId === 'ex1-tabs-3') {
      new Glider(document.getElementById('glide-3'), {
        slidesToScroll: 1,
        slidesToShow: 3.7,
        draggable: true,
        arrows: {
          prev: '.glider-prev-3',
          next: '.glider-next-3'
        },
        responsive: [
          {
            breakpoint: 1200,
            settings: {
              slidesToShow: 3.7,
              slidesToScroll: 2,

            }
          }, {
            breakpoint: 900,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 640,
            settings: {
              slidesToShow: 2,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 304,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,

            }
          },
          {
            breakpoint: 0,
            settings: {
              slidesToShow: 1,
              slidesToScroll: 1,

            }
          }

        ]
      });
      // Manipulate the CSS styles
      $('#glider1').css('display', 'none');
      $('#glider2').css('display', 'none');
      $('#glider3').css('display', 'flex');
    }
  });

  $('#ex1-tab-1').on('shown.bs.tab', function (e) {
    var tabId = e.target.getAttribute('aria-controls'); // Get the ID of the active tab
    if (tabId === 'ex1-tabs-1') {
      // Write your Glider initialization code here if necessary

      // Manipulate the CSS styles
      $('#glider1').css('display', 'flex');
      $('#glider2').css('display', 'none');
      $('#glider3').css('display', 'none');
    }
  });

  // Media Slider End

