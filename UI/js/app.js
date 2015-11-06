'use strict';

var app = Sammy('#viewContainer', function () {

	this.get('#/', function () {
		this.partial('views/enterApp.html');
	});

	this.get('#/home', function () {
		this.partial('views/home.html')
			.then(function () {
				offersCtrl.render(3, 6);
				carouselCtrl.renderCarousel();
				categoryMenuCtrl.render();
			});

	});

	this.get('#/addOffer', function () {
		var currentUser = Parse.User.current();
		if (currentUser) {
			this.partial('views/newOffer.html')
			.then(function () {
				newOfferCtrl();
			})
		} else {
			window.location.href = '#/userLogin';
		}
		
	})

	this.get('#/userLogin', function () {
		this.partial('views/userLogin.html')
			.then(function () {
				registerUserCtrl.render();
			});
	});
	
	this.get('#/offerDetails/:id', function () {
		var id = this.params['id'].substring(1);
		
		this.partial('views/offerDetails.html')
			.then(function () {
				offerDetailsCtrl.render(id);
			});
	});

	this.get('#/offers/:category', function () {
		var category = this.params['category'].substring(1);

		this.partial('views/offers.html')
			.then(function () {
				var sortBy = 'newest';
				if(localStorage.getItem('sortBy') !== null){
					var sortBy = localStorage.getItem('sortBy');
				}
				offersCtrl.render(0, 9, category, sortBy, true);
				categoryMenuCtrl.render();
			});
	});
});
app.run('#/');