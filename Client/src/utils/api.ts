import ky from 'ky';

const api = ky.create({
    prefixUrl: process.env.NEXT_PUBLIC_SERVER_BASE_URL || 'http://localhost:5291',
    headers: {
        'Content-Type': 'application/json',
    },
    hooks:{
        beforeRequest: [
            (request) => {
                console.log(`Server base URL: ${process.env.NEXT_PUBLIC_SERVER_BASE_URL}`);
            },
        ],
    },
});

export async function changePassword(payload: { password: string; }): Promise<any> {
    return api.post('password/change', { json: payload }).json();
}

export async function get(): Promise<any> {
    return api.get('password/get').json();
}

