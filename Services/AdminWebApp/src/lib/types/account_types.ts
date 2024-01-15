interface RegisterResponse {
	success: boolean;
}

interface RegisterErrors {
	type: string;
	title: string;
	status: number;
	detail: string;
	instance: string;
	errors: {
		[key: string]: string[];
	};
}

interface LoginResponse {
	tokenType: string;
	accessToken: string;
	expiresIn: number;
	refreshToken: string;
}

interface LoginErrors {
	type: string;
	title: string;
	status: number;
	detail: string;
}

interface User {
	id: string;
	userName: string;
	fullName: string;
	profileImageName: string;
	email: string;
}

export type { LoginErrors, LoginResponse, RegisterErrors, RegisterResponse, User };
