class Constants {

  static BASE_URL = '/';
}

class Api {

  static get(url, data) {
    return this.ajax('GET', url, data);
  }

  static post(url, data) {
    return this.ajax('POST', url, data, true);
  }

  static put(url, data) {
    return this.ajax('PUT', url, data, true);
  }

  static del(url, data) {
    return this.ajax('DELETE', url, data, true);
  }

  static ajax(type, url, data) {
    let settings = {
      type: type,
      url: url,
      cache: false
    };

    if (data) {
      settings.data = data;
    }

    return $.ajax(settings);
  }
}