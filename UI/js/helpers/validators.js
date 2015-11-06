var validator = (function () {

	var PASSWORD_CONSTRAINTS = {
		min: 3,
		max: 22
	};

	function validateManufacturer() {
		var manufacturer = $('#manufacturer');

		manufacturer.on('input', function () {
			if (manufacturer.val() == null || manufacturer.val() == "") {
				manufacturer.parent().parent().addClass('has-error');
			} else {
				manufacturer.parent().parent().removeClass('has-error');
			}
		});
	}

	function validateModel() {
		var model = $('#model');

		model.on('input', function () {
			if (model.val() == null || model.val() == "") {
				model.parent().parent().addClass('has-error');
			} else {
				model.parent().parent().removeClass('has-error');
			}
		});
	}

	function validateYear() {
		var year = $('#year');

		year.on('input', function () {
			if (year.val() == null || year.val() < 1886 || year.val() > new Date().getFullYear()) {
				year.parent().parent().addClass('has-error');
			} else {
				year.parent().parent().removeClass('has-error');
			}
		});
	}

	function validatePrice() {
		var price = $('#price');

		price.on('input', function () {
			if (price.val() == null || price.val() < 0 || price.val() == "") {
				price.parent().parent().parent().addClass('has-error');
			} else {
				price.parent().parent().parent().removeClass('has-error');
			}
		});
	}

	function validateForm() {
		validateManufacturer();
		validateModel();
		validateYear();
		validatePrice();
	}

	function validateFormForSubmit() {
		return !(
			$('#manufacturer').parent().parent().hasClass('has-error')
			|| $('#model').parent().parent().hasClass('has-error')
			|| $('#price').parent().parent().hasClass('has-error')
			|| $('#year').parent().parent().hasClass('has-error')
			|| $('#image').parent().parent().hasClass('has-error'));
	}

	function validatePassword(password, passwordConfirm) {
		password = password + '';
		passwordConfirm = passwordConfirm + '';
		if (password != passwordConfirm) {
			$('#userRegisterMessages').text('Passwords do not match!!!');
			return false;
		}
		if (password.length < PASSWORD_CONSTRAINTS.min || password.length > PASSWORD_CONSTRAINTS.max) {
			$('#userRegisterMessages').text('Password is too long or too short!!!');
			return false;
		}
		return true;
	}

	function validateUserNames(firstName, lastName) {
		if (firstName == null || lastName == null
			|| firstName.length < 1 || lastName.length < 1) {
			$('#userRegisterMessages').text('First name and lastname are mandatory!');
			return false;
		}
		if (typeof firstName != 'string' || typeof lastName != 'string') {
			$('#userRegisterMessages').text('First name and lastname should be text!');
			return false;
		}
		return true;
	}

	function validateUserEmail(email) {
		var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
		return re.test(email);
	}

	return {
		validateForm: validateForm,
		validateFormForSubmit: validateFormForSubmit,
		validatePassword: validatePassword,
		validateUserNames: validateUserNames,
		validateUserEmail: validateUserEmail
	}
} ())