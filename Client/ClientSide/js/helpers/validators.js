var validator = (function () {

	var PASSWORD_CONSTRAINTS = {
		min: 6,
		max: 30
	};

	function validateTextField(text) {
		if (text == null || text == "" || text == undefined) {
			return false;
		};

		return true;
	}

	function validatePrice(price) {
		if (price >= 0) {
			return true;
		};

		return false;
	}

	function validateDate(date) {
		if (new Date(date) > Date.now()) {
			return false;
		};

		return true;
	}

	function validatePasswordConformation(password, passwordConfirm) {
		password = password + '';
		passwordConfirm = passwordConfirm + '';
		if (password != passwordConfirm) {
			return false;
		}
		return true;
	}

	function validatePasswordLength(password)
	{
		if (password.length < PASSWORD_CONSTRAINTS.min) {
			return false;
		}
		return true;
	}

	function validateUserNames(firstName, lastName) {
		if (firstName == null || lastName == null
			|| firstName.length < 1 || lastName.length < 1) {
			return false;
		}
		if (typeof firstName != 'string' || typeof lastName != 'string') {
			return false;
		}
		return true;
	}

	function validateUserEmail(email) {
		var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
		return re.test(email);
	}

	return {
		validatePasswordConformation: validatePasswordConformation,
		validateUserNames: validateUserNames,
		validateUserEmail: validateUserEmail,
		validatePasswordLength: validatePasswordLength,
		validateTextField: validateTextField,
		validatePrice: validatePrice,
		validateDate: validateDate
	}
} ())