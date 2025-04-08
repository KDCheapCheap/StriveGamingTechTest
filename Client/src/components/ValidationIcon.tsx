//Reuseable Component for validation icon
const ValidationIcon: React.FC<{ condition: boolean }> = ({ condition }) => {
    return condition ? (
        <span className="text-green-500 bi-check-lg"></span>
    ) : (
        <span className="text-red-500 bi-x-lg"></span>
    );
};