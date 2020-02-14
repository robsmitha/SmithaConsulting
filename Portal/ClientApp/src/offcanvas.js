document.addEventListener('DOMContentLoaded', function () {
  //document.querySelectorAll('[data-toggle="offcanvas"]').click = function () {
  //  document.getElementsByClassName('offcanvas-collapse').toggle('open')
  //}

  var navbar = document.querySelector('#main_nav').clientHeight;
  document.querySelector('body').style = 'padding-top: ' + navbar + 'px';

}, false);

