interface TextFieldProps {
  label: string;
  name: string;
  value: string;
  type?: string;
  min?: string;
  step?: string;
  error?: string;
  onChange: (name: string, value: string) => void;
}

export function TextField({ label, name, value, type = 'text', min, step, error, onChange }: TextFieldProps) {
  return (
    <label className="field">
      <span>{label}</span>
      <input
        name={name}
        value={value}
        type={type}
        min={min}
        step={step}
        onChange={event => onChange(name, event.target.value)}
      />
      {error && <small>{error}</small>}
    </label>
  );
}
