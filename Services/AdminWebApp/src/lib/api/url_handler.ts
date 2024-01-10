const urlHandler = {
	getBaseUrl(url: string) {
		if (process.env.NODE_ENV === 'development') {
			return 'http://localhost:5000/api' + url;
		} else {
			return 'http://netmon-api-gateway:80/api' + url;
		}
	},
	getDeviceManagerUrl(url: string) {
		return this.getBaseUrl('/device' + url);
	}
};

export default urlHandler;
