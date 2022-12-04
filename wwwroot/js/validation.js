
$(document).ready(() => {
  $.ajaxSetup({
    method: 'POST',
    dataType: "JSON",
  });
});

function isEmailValid(email) {
  const validateEmail = (email) => {
    return email.match(
      /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
  };
  return validateEmail(email) != null;
}

function addInvalidField(field) {
  field.removeClass("border-gray-300").addClass('border-red-500');
}