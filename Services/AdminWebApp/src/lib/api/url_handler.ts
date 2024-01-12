const urlHandler = {
	getBaseUrl(url: string) {
		return (process.env.BASE_URL || "http://localhost:5000/api") + url; 
	},
	getDeviceManagerUrl(url: string) {
		return this.getBaseUrl('/device' + url);
	}
};

export default urlHandler;
