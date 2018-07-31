function getAllQueryString() {

    var all = window.location.search.substr(1).split("&");
    var fq = {};
    for (var i = 0; i < all.length; i++) {
        fq[all[i].split("=")[0]] = unescape(all[i].split("=")[1]);
    }
    return fq;
}

function getAllQueryResolveString() {
    var params = getAllQueryString();
    var key = params['key'];
    if (key) {
        var rp = {};
        $.each(params, function (n, v) {
            if (n != 'key') {
                rp[n] = v.decrypt(key);
            }
        });
        return rp;
    }
    return params;
}