var offerDetailsCtrl = (function () {

	function render(offerId) {
		Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");

		var Offer = Parse.Object.extend('Offer');
		var query = new Parse.Query(Offer);

		query.equalTo('objectId', offerId);

		query.find({
			success: function (offer) {
				var offerDetailsTemplate = $('#offer-details').html();
				var container = $('#offer-container');

				var outputOfferHtml = Mustache.render(offerDetailsTemplate, offer[0]._serverData);
				container.append(outputOfferHtml);

				renderSellerInfo(offer[0]._serverData.createdBy.id);
				handleFbShare(offer[0]._serverData);
				addDeleteButton(offer);
			}
		}).then(function (offer) {
			$.getScript("lib/jquery.rateit.min.js");
			bindRating(offer[0]);
		});
	}

	function addDeleteButton(offer) {
		if (Parse.User.current()) {
			if (offer[0]._serverData.createdBy.id === Parse.User.current().id) {
				var deleteBtn = $('<button/>').addClass('btn btn-danger pull-right btn-lg').html('Delete offer');

				deleteBtn.on('click', function () {
					offer[0].destroy({
						success: function (offer) {
							window.history.back();
						}
					});
				})

				$('.panel-body').append(deleteBtn);
			}
		}
	}

	function renderSellerInfo(sellerId) {
		var User = Parse.Object.extend('User');
		var query = new Parse.Query(User);

		query.equalTo('objectId', sellerId);
		query.find({
			success: function (seller) {
				var sellerDetailsTemplate = $('#seller-details').html();
				var container = $('#seller-info');

				var outputSellerHtml = Mustache.render(sellerDetailsTemplate, seller[0]._serverData);
				container.append(outputSellerHtml);
			}
		})
	}

	function handleFbShare(offer) {
		$('#share').on('click', function () {
			FB.ui({
				method: 'feed',
				link: window.location.href,
				name: offer.Manufacturer + ' ' + offer.Model,
				description: offer.Description,
				picture: offer.imageURL
			}, function (response) { });
		});
	}

	function bindRating(offer) {
		$('.rateit').bind('rated', function () {
			var rating = $(this),
				value = rating.rateit('value'),
				currentRating = offer._serverData.Rating,
				numberOfRatings = offer._serverData.numberOfRatings;
		
			currentRating = (currentRating * numberOfRatings + value) / (numberOfRatings + 1);
			
			offer.set('Rating', currentRating);
			offer.set('numberOfRatings', ++numberOfRatings);
			offer.save();			
			
			rating.rateit('readonly', true);
		});
	}

	return {
		render: render
	}
} ())