Handlebars.registerHelper('if_eq_num', function(a, b, opts) {
    if(Math.round(a) == Math.round(b))
        return opts.fn(this);
    else
        return opts.inverse(this);
});