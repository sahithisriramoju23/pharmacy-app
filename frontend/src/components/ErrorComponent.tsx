type ErrorComponentProps = {
    errorMessage: string;
}
const AlertIcon = () => (
  <svg className="w-8 h-8 mx-auto mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4v.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
  </svg>
);
export const ErrorComponent = ({ errorMessage }: ErrorComponentProps) => {
  return (
      <div className="flex items-center justify-center min-h-[400px]">
        <div className="text-center text-red-600">
          <AlertIcon />
          <p>Error: {errorMessage}</p>
        </div>
      </div>
    );
}
