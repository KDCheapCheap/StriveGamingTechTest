'use client'

import Image from "next/image";
import React, { useState } from "react";

export default function Home() {

    //State vars for password and confirm password.
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    //State var for password validation.
    const [passwordValidation, setPasswordValidation] = useState({
        length: false,
        hasNumber: false,
        hasSpecialChar: false,
        allowedChars: false,
        isIdentical: false,
        isValid: false,
    });

    function handlePasswordFieldChange(e: React.ChangeEvent<HTMLInputElement>) {
        setPassword(e.target.value);
        ValidatePassword(e.target.value, confirmPassword);
    }

    function handleConfirmPasswordFieldChange(e: React.ChangeEvent<HTMLInputElement>) {
        setConfirmPassword(e.target.value);
        ValidatePassword(password, e.target.value);
    }

    function ValidatePassword(password: string, confirmPassword: string) {
        const length = /^(?=.{7,14}$)/.test(password);
        const hasNumber = /(?=.*[0-9])/.test(password);
        const hasSpecialChar = /(?=.*[!£$^*#])/.test(password);
        const allowedChars = /^[a-zA-Z0-9!£$^*#]*$/.test(password);
        const isIdentical = password === confirmPassword;

        setPasswordValidation({
            length,
            hasNumber,
            hasSpecialChar,
            allowedChars,
            isIdentical,
            isValid: length && hasNumber && hasSpecialChar && allowedChars && isIdentical,
        });
    }

    async function handleFormSubmit(e: React.MouseEvent<HTMLButtonElement>) {
        e.preventDefault();
        console.log('Post password to back end');
    }

    return (
        <>
            <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                    <Image src="/strive-logo.jpg" alt="Strive Gaming" height="110" width="110" className="mx-auto" />
                    <h2 className="text-center text-2xl font-bold leading-9 tracking-tight text-gray-900"
                        data-testid="title">
                        Change your password
                    </h2>
                </div>

                <div className="mt-4 sm:mx-auto sm:w-full sm:max-w-sm">
                    <form className="space-y-6">
                        <div>
                            <label htmlFor="password"
                                className="block text-sm font-medium leading-6 text-gray-900">
                                New password
                            </label>
                            <div className="mt-2">
                                <input
                                    id="password"
                                    name="password"
                                    type="password"
                                    data-testid="password"
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={handlePasswordFieldChange}
                                />
                            </div>
                        </div>

                        <div>
                            <div className="flex items-center justify-between">
                                <label htmlFor="confirm" className="block text-sm font-medium leading-6 text-gray-900">
                                    Re-type new password
                                </label>
                            </div>
                            <div className="mt-2">
                                <input
                                    id="confirm"
                                    name="confirm"
                                    type="password"
                                    className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                    onChange={handleConfirmPasswordFieldChange}
                                />
                            </div>
                        </div>
                        <div>
                            <button
                                className={`flex w-full justify-center px-4 py-2 rounded-md text-white ${passwordValidation.isValid
                                    ? "bg-gray-900 hover:bg-gray-700 active:bg-gray-800"
                                    : "bg-gray-400 cursor-not-allowed"
                                    }`}
                                onClick={(e) => handleFormSubmit(e)}
                                disabled={!passwordValidation.isValid}>
                                Submit
                            </button>
                        </div>
                    </form>
                </div>

                {password !== "" && (
                    <div className="mx-auto text-xs mt-8">
                        <ol>
                            <li>
                                Password must be between 7-14 characters in length <ValidationIcon condition={passwordValidation.length} />
                            </li>
                            <li>
                                Password must contain at least 1 number and one special character <ValidationIcon condition={passwordValidation.hasNumber && passwordValidation.hasSpecialChar} />
                            </li>
                            <li>
                                Password does not contain special characters other than <code>!£$^*#</code> <ValidationIcon condition={passwordValidation.allowedChars} />
                            </li>
                            <li>
                                Both passwords must be identical <ValidationIcon condition={passwordValidation.isIdentical} />
                            </li>
                        </ol>
                    </div>
                )}
            </div>
        </>
    );
}


