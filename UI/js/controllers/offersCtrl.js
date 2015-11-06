var offersCtrl = (function () {
	function render(skippedOffersCount, offersLimit, category, sortBy, loadMoreOffers) {
		Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");

		var Offer = Parse.Object.extend('Offer'),
			query = new Parse.Query(Offer),
			shownOffersCount = offersLimit;

		if (skippedOffersCount) {
			query.skip(skippedOffersCount);
		}

		if (offersLimit) {
			query.limit(offersLimit);
		}

		if (category && category != 'all') {
			if (category === 'myOffers') {
				query.equalTo('createdBy', Parse.User.current());
			} else {
				query.equalTo('Category', category);
			}
		}

		$('#sortBy').val(sortBy);
		var text = $("select[name=selValue] option[value=" + sortBy + "]").text();
		$('.bootstrap-select .filter-option').text(text);

		switch (sortBy) {
			case 'priceAsc':
				query.ascending("Price");
				break;
			case 'priceDesc':
				query.descending("Price");
				break;
			case 'ratingAsc':
				query.ascending("Rating");
				break;
			case 'ratingDesc':
				query.descending("Rating");
				break;
			case 'newest':
				query.descending("createdAt");
				break;
			case 'oldest':
				query.ascending("createdAt");
				break;
			default:
				query.descending("createdAt");
				break;
		}

		$('#sortBy').off().on('change', function () {
			var sortBy = $('#sortBy').val();
			localStorage.setItem('sortBy', sortBy);
			offersCtrl.render(0, 9, category, sortBy, true);
			return;
		});

		query.find({
			success: function (offers) {
				var container = $('#offers-container');
				container.empty();

				if (offers.length > 0) {
					var offerThumbnailTemplate = $('#offer-thumbnail').html();

					offers.forEach(function (offer) {
						var outputOfferHtml = Mustache.render(offerThumbnailTemplate, offer);
						container.append(outputOfferHtml);
					});

					$('.thumbnail').on('click', function (ev) {
						var url = $(ev.target).parents('.thumbnail').find('a').attr('href');
						window.location.href = url;
					})
				} else {
					var noOffersAlert = $('<div/>').addClass('alert alert-dismissible alert-danger').html('There are no offers to be shown.');

					container.append(noOffersAlert);
				}
			}
		}).then(function () {
			$.getScript("lib/jquery.rateit.min.js");
		});

		if (loadMoreOffers) {
			$(window).off('scroll').on('scroll', function () {
				var pageHeight = $('#wrap').outerHeight();
				var y = window.pageYOffset + window.innerHeight;

				if (y >= pageHeight) {
					query = new Parse.Query(Offer);

					query.skip(shownOffersCount);
					query.limit(9);

					shownOffersCount += 9;

					if (category && category != 'all') {
						if (category === 'myOffers') {
							query.equalTo('createdBy', Parse.User.current());
						} else {
							query.equalTo('Category', category);
						}
					}

					switch (sortBy) {
						case 'priceAsc':
							query.ascending("Price");
							break;
						case 'priceDesc':
							query.descending("Price");
							break;
						case 'ratingAsc':
							query.ascending("Rating");
							break;
						case 'ratingDesc':
							query.descending("Rating");
							break;
						case 'newest':
							query.descending("createdAt");
							break;
						case 'oldest':
							query.ascending("createdAt");
							break;
						default:
							query.descending("createdAt");
							break;
					}

					query.find({
						success: function (offers) {
							var container = $('#offers-container');

							if (offers.length > 0) {
								var offerThumbnailTemplate = $('#offer-thumbnail').html();

								offers.forEach(function (offer) {
									var outputOfferHtml = Mustache.render(offerThumbnailTemplate, offer);
									container.append(outputOfferHtml);
								});

								$('.thumbnail').on('click', function (ev) {
									var url = $(ev.target).parents('.thumbnail').find('a').attr('href');
									window.location.href = url;
								})
							}
						}
					}).then(function () {
						$.getScript("lib/jquery.rateit.min.js");
					});
				}
			})
		}
	};

	return {
		render: render
	}
} ());