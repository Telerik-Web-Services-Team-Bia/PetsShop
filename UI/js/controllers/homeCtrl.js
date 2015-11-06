var homeCtrl = (function () {
	return {
		render: function () {
			Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");

			var Offer = Parse.Object.extend('Offer');
			var query = new Parse.Query(Offer);

			query.find({
				success: function (offers) {
					var offerThumbnailTemplate = $('#offer-thumbnail').html();
					var container = $('#offers-container');
					
					offers.forEach(function (offer) {
						var outputOfferHtml = Mustache.render(offerThumbnailTemplate, offer._serverData);
						container.append(outputOfferHtml);
					});
				}
			});			
		}
	}
} ());