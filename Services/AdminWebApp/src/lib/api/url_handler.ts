const urlHandler = {
	getBaseUrl(url: string) {
		if (process.env.NODE_ENV === 'development') {
			return 'http://localhost:5000/api' + url;
		} else {
			return 'http://192.168.178.254:5000/api' + url;
		}
	},
    getDeviceManagerUrl(url: string) {
        return this.getBaseUrl('/device' + url);
    }
};

export default urlHandler;
