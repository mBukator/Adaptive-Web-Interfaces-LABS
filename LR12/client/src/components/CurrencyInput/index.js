import axios from 'axios'
import React, { useState } from 'react'

const CurrencyInput = () => {
	const [currency, setCurrency] = useState('')

	const handleSubmit = async (event) => {
		event.preventDefault()

		try {
			const response = await axios.get(
				`https://localhost:7228/get-currency-rate?currency=${currency}`,
			)
			console.log(response.data)
		} catch (error) {
			console.error('Error when sending a request:', error)
		}
	}

	return (
		<>
			<form onSubmit={handleSubmit}>
				<input
					type='text'
					value={currency}
					onChange={(e) => setCurrency(e.target.value)}
					placeholder='Enter the currency code (e.g. USD or EUR)'
					required
				/>
				<button type='submit'>Submit</button>
			</form>
		</>
	)
}

export default CurrencyInput
