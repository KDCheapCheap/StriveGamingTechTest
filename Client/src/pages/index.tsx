'use client'

import Image from "next/image";
import React, {useState} from "react";

export default function Home() {

    function handlePasswordFieldChange(e: React.ChangeEvent<HTMLInputElement>) {
        console.log('Password field changed');
    }

    async function handleFormSubmit(e: React.MouseEvent<HTMLButtonElement>) {
        e.preventDefault();
        console.log('Post password to back end');
    }

    return (
        <>
            <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
                <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                    <Image src="/strive-logo.jpg" alt="Strive Gaming" height="110" width="110" className="mx-auto"/>
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
                                />
                            </div>
                        </div>
                        <div>
                            <button
                                className={"flex w-full justify-center bg-gray-900 hover:bg-gray-700 active:bg-gray-800 px-4 py-2 rounded-md text-white"}
                                onClick={(e) => handleFormSubmit(e)}>
                                Submit
                            </button>
                        </div>
                    </form>
                </div>

                <div className="mx-auto text-xs mt-8">
                    <ol>
                        <li>
                            Password must be between 7-14 characters in length
                        </li>
                        <li>
                            Password must contain at least 1 number and one special characters
                        </li>
                        <li>
                            Password does not contain special characters other than <code>!£$^*#</code>
                        </li>
                        <li>
                            Both passwords must be identical
                        </li>
                    </ol>
                </div>
            </div>
        </>
    );
}
