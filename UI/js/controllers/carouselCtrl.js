var carouselCtrl = (function() {
	
	function renderCarousel() {
		Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");

		var Offer = Parse.Object.extend('Offer');
		var query = new Parse.Query(Offer);

		query.limit(3);
		
		query.descending('numberOfRatings');

		query.find({
			success: function (offers) {
				var container = $('.carousel-holder'),
					carouselTemplate = $('#carousel-template').html();

				var outputCarouselHtml = Mustache.render(carouselTemplate, offers);

				container.append(outputCarouselHtml);

				$('.item').on('click', function (ev) {
					var offerId = $(ev.target).parents('.item').attr('offerId');

					window.location.href = '#/offerDetails/:' + offerId;
				})
			}
		});
	};
	
	return {
		renderCarousel: renderCarousel
	}
}())